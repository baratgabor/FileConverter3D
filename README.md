# FileConverter3D

Extensible, loosely coupled file converter scaffolding that is (hopefully) reusable for additional 3D file formats.

Currently can read/write ASCII Obj and binary STL.

Has transformation matrix based mesh transformation capability that doesn't have any external dependencies.

# Current status

Under development, but working/functional. The basic structure is semi-solid already, so I'll start adding tests soon.

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

Currently the functionality is exposed in an easy to consume way, through static methods in the `FileConverter3D` class. This static class serves as the composition root of the converter graph.

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
