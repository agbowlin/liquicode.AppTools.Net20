

# liquicode.AppTools.Net20

A series of small .NET 2.0 libraries which aid in the development of
applications.

All projects exist in the `liquicode.AppTools` namespace and each exports one
or more classes to expose underlying functionality. Some projects expose a
single static class (e.g. `Imaging`), some expose a number of creatable
classes, and others expose a combination of static and dynamically creatable
classes. 

All of these projects are straight up .NET 2.0 compatible and have no 3rd
party dependencies.
My intention for setting this restriction is to make the code as portable as
possible.
Also, the wrapping of 3rd party libraries is the focus of another project:
[liquicode.LibWraps.Net](https://github.com/agbowlin/liquicode.LibWraps.Net)

Note that the following classes access a Windows system dll and, therefore,
would not be portable to other operating systems:

- `liquicode.AppTools.WinDpapi`


# liquicode.AppTools Projects

------------------------------------------

### liquicode.AppTools.Ciphers

- `liquicode.AppTools.Ciphers`
- `liquicode.AppTools.WinDpapi`

------------------------------------------

### liquicode.AppTools.DataManagement

- `liquicode.AppTools.DataManagement`
- `liquicode.AppTools.DataTableHelper`
- `liquicode.AppTools.Files`
- `liquicode.AppTools.Identity`
- `liquicode.AppTools.Strings`
- `liquicode.AppTools.XmlObjectPersonalizedSerializer`
- `liquicode.AppTools.XmlObjectSerializer<T>`

------------------------------------------

### liquicode.AppTools.DataStructures

- `liquicode.AppTools.DataStructures`

------------------------------------------

### liquicode.AppTools.Dictionaries

------------------------------------------

### liquicode.AppTools.FileSystem

------------------------------------------

### liquicode.AppTools.Imaging

------------------------------------------

### liquicode.AppTools.Parsing

------------------------------------------

### liquicode.AppTools.Sockets

------------------------------------------

### liquicode.AppTools.VisualComponents

------------------------------------------

### liquicode.AppTools.Windowing

