# FileConverter3D

Extensible, loosely coupled file converter scaffolding that is (hopefully) reusable for additional 3D file formats.

Currently can read/write ASCII Obj and binary STL.

Has transformation matrix based mesh transformation capability that doesn't have any external dependencies.

Not really optimized, besides "don't be an idiot" sort of basic concerns.

(I think this structure makes it feasible to implement direct transcoding with a sliding window (without loading the full model into memory). It could still support transformations too, since a transformation decorator could be used on top of the vertex and normal stream. I might look into this later.)

# Current status

Unstable, under development, but working/functional. The basic structure is semi-solid already, so I'll start adding tests soon.

# Who's this useful for

Potentially useful for you if you're looking for some ideas with regards to how to structure a generalized converter to be able to reuse part of the created components.

This scaffolding divides the conversion into single responsibilities like file reading, data parsing, and writing into a model; expressing this structure through `interface` based contracts and a central model format. The result is that certain components can be reused for multiple file formats, plus you can flexibly use decorators and other wrappers in the composition graph. 

For example, the OBJ importer graph looks like this; you can see that it incorporates both OBJ-specific and common components:

```csharp
var objImporter =
    new Common.FileImporter<string>(
        new Common.ReadingStream(),
        new ObjAscii.LineFilterProxy(
            new Common.TextLineReader()),
        new Common.ParserChain<string>(
            new ObjAscii.VertexParser(),
            new ObjAscii.NormalParser(),
            new ObjAscii.TextureCoordParser(),
            new ObjAscii.FaceParser()),
        new ObjAscii.ModelWriterDecorator_FaceIndexCorrection(
            new Common.ModelWriter(() => new Model())))
```

And the actual import process happens in a reusable `FileImporter<T>` facade class the following way:

```csharp
     public IModel Import(string filePath) => 
          _modelWriter.Write(
              _valueParser.Parse(
                  _valueReader.Read(
                      _fileStreamer.GetStream(filePath))));
```

# Usage

## Code
Currently the functionality is exposed in an easy to consume way, through static methods in the `FileConverter3D` class. This static class serves as the composition root of the converter graph. No other parts of the code contain references to concrete implementations; besides the model structs/classes which are considered data containers.

Example:

```csharp
  // Import
  var model = FileConverter3D.Import.ObjAscii("c:\mesh.obj");

  // Transform
  FileConverter3D.Transform.GetModelTransform()
      .AddScale(2, 2, 2)
      .AddTranslation(5, 10, 0)
      .AddRotation(47, 72, 18)
      .Apply(model);

  // Analyze
  Console.WriteLine("Model surface area: " + FileConverter3D.Analyze.CalculateSurfaceArea(model));
  Console.WriteLine("Model volume: " + FileConverter3D.Analyze.CalculateVolume(model));
  
  // Export
  FileConverter3D.Export.StlBinary(model, "c:\mesh.stl");
```

## Console interface

The console app in the `FileConverter3D.Console` assembly exposes operations in two ways: traditional command line arguments, and interactive mode (when no arguments are specified). Both mode supports endless chaining of operations, and the console runner interprets and executes them in order.

**Available commands and their syntax:**

`import [objascii|stlbinary] "File path"`

`export [objascii|stlbinary] "File path"`

`translate x y z`

`rotate x y z`

`scale x y z`

`overwritemode`

**Example use:**

`FileConverter3D.Console.exe overwritemode  import objascii "c:\models\obj model.obj"  scale 2 2 2  rotate 180 0 0  translate 2.5 0 0  export stlbinary "c:\models\stl model.stl"`

(If you run the executable without command line arguments, it starts in interactive mode, where you can type in and execute commands sequentially. Although interactive mode also supports specifying multiple commands in a single line.)
