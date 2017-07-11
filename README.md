# Don't ever use this

it's an abomination


# Examples
someobj.json
```json
{
    "name" : "SomeObject",
    "fields" : [
        { "name" : "Name", "access" : "public", "type" : "System.String" },
        { "name" : "Age", "access" : "public", "type" : "System.Int32" },
        { "name" : "Inside", "access" : "public", "type" : "InnerObject" }
    ]
}
```
innerobj.json
```json
{
    "name" : "InnerObject",
    "fields" : [
        { "name" : "Str", "type" : "System.String" },
        { "name" : "I32", "type" : "System.Int32" },
    ]
}
```
If an object contains a different object that is defined in JSON, they must be compiled together.
