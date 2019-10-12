## Summary

Computes chains of mathematical operations as provided in instuctions file

## Usage

```
dotnet run --project ./numeric-operator/. --source-file ./stubs/example1.txt
```

or

```
docker build . -t op
docker run -v %cd%/stubs:/stubs op --source-file /stubs/example1.txt
```