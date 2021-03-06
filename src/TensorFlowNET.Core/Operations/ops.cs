﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Tensorflow;
using node_def_pb2 = Tensorflow;
using Google.Protobuf;

namespace Tensorflow
{
    public static class ops
    {
        public static Graph get_default_graph()
        {
            return tf.Graph();
        }

        public static Tensor convert_to_tensor()
        {
            return internal_convert_to_tensor();
        }

        private static Tensor internal_convert_to_tensor()
        {
            return null;
        }



        public static unsafe IntPtr _create_c_op(Graph graph, NodeDef node_def, List<Tensor> inputs)
        {
            var op_desc = c_api.TF_NewOperation(graph.Handle, node_def.Op, node_def.Name);

            // Add inputs
            if(inputs != null)
            {
                foreach (var op_input in inputs)
                {
                    c_api.TF_AddInput(op_desc, op_input._as_tf_output());
                }
            }

            var status = new Status();

            // Add control inputs

            // Add attrs
            foreach (var attr in node_def.Attr)
            {
                var bytes = attr.Value.ToByteArray();
                var proto = Marshal.AllocHGlobal(bytes.Length);
                Marshal.Copy(bytes, 0, proto, bytes.Length);
                c_api.TF_SetAttrValueProto(op_desc, attr.Key, proto, proto_len: (UIntPtr)bytes.Length, status: status.Handle);

                if(status.Code != TF_Code.TF_OK) throw new Exception(status.Message);
            }

            var c_op = c_api.TF_FinishOperation(op_desc, status.Handle);

            if (status.Code != TF_Code.TF_OK) throw new Exception(status.Message);

            return c_op;
        }

        public static NodeDef _NodeDef(string op_type, string name, string device = "", Dictionary<string, AttrValue> attrs = null)
        {
            var node_def = new node_def_pb2.NodeDef();
            node_def.Op = op_type;
            node_def.Name = name;

            foreach (var attr in attrs)
            {
                node_def.Attr.Add(attr.Key, attr.Value);
            }
            
            return node_def;
        }

        public static string _name_from_scope_name(string name)
        {
            if (name.EndsWith("/"))
            {
                return name.Substring(0, name.Length - 1);
            }
            else
            {
                return name;
            }
        }

        public static int uid()
        {
            return 1;
        }
    }
}
