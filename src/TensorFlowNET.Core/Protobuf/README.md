﻿### Download compiler from https://github.com/protocolbuffers/protobuf/releases
```shell
set SRC_DIR=D:\Projects\tensorflow\tensorflow\core\framework
set DST_DIR=D:\Projects\TensorFlow.NET\src\TensorFlowNET.Core\Tensorflow

.\protoc -I=%SRC_DIR% --csharp_out=%DST_DIR% resource_handle.proto
.\protoc -I=%SRC_DIR% --csharp_out=%DST_DIR% tensor_shape.proto
.\protoc -I=%SRC_DIR% --csharp_out=%DST_DIR% types.proto
.\protoc -I=%SRC_DIR% --csharp_out=%DST_DIR% tensor.proto
.\protoc -I=%SRC_DIR% --csharp_out=%DST_DIR% attr_value.proto
.\protoc -I=%SRC_DIR% --csharp_out=%DST_DIR% node_def.proto
```