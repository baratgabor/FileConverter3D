# FileConverter3D

Extensible, loosely coupled file converter scaffolding that is (hopefully) reusable for additional 3D file formats.

Currently can read/write ASCII Obj and binary STL.

Has transformation matrix based mesh transformation capability that doesn't have any external dependencies.

Not really optimized, besides "don't be an idiot" sort of basic concerns.

(I think this structure makes it feasible to implement direct transcoding with a sliding window (without loading the full model into memory). It could still support transformations too, since a transformation decorator could be used on top of the vertex and normal stream. I might look into this later.)

# Current status

Soon **stable** for the current feature set, but unit-testing coverage is extremely poor at the moment.

## Currently supported 3D file format features

#### ASCII Obj

- Vertices (*'v'*), vertex normals (*'vn'*), vertex textures (*'vt'*), and faces (*'f'*).
- Supports arbitrary number of face vertices.
- Supports relative/negative indexes. Converts these into absolute/positive indexes during import.

#### Binary STL

- All features, since this format is simply a list of triangles and their normal. *(During STL export the converter triangulates the (possibly non-tri) faces in the model, and calculates a new face normal. This face normal is currently a primitive face-orthogonal normal, which means this creates a 'faceted' look.)*

## Currently supported transformation features

The matrix transformation system implemented has the following characteristics:

- Supports **rotation** by an arbitrary 3D vector of floats, in degrees.
- Supports **scaling** by an arbitrary 3D vector of floats.
- Supports **translation** by an arbitrary 3D vector of floats.
- Transformations are **applied in the correct order**, i.e. rotate/scale first, translate after.
- Transformations are **accumulated** into a matrix, and applied all at once.
- The **integrity of normals is preserved** during transformation (it calculates a special matrix - the transpose of the inverted transformation matrix - for transforming the normals).

# Who's this useful for

Potentially useful for you if you're looking for some ideas with regards to how to structure a generalized converter to be able to reuse part of the created components.

This scaffolding divides the conversion into single responsibilities like file reading, data parsing, and writing into a model; expressing this structure through `interface` based contracts and a central model format. The result is that certain components can be reused for multiple file formats, plus you can flexibly use decorators and other wrappers in the composited graph. 

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

**Available commands and their syntax (list accessible in the app using 'help'):**

`import [objascii|stlbinary] "File path"`

`export [objascii|stlbinary] "File path"`

`translate x y z`

`rotate x y z`

`scale x y z`

`overwritemode`

**Usage example in interactive mode:**

![FileConverter3D console app usage example screenshot](C:\Users\barat\source\repos\FileConverter3D\docs\ConsoleApp.png)

As you can observe here too, you can issue multiple commands on the same line, if you want.

You can pass the same command line as an argument to the `.exe`, in which case it executes the commands parsed from that single command line, then exits.

File path can be specified without quotes if it doesn't contain spaces, or enclosed in quotes if it does contain spaces.