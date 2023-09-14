# learn2blog

A command-line app to convert a text file to HTML

## Building and Running the App

To build and run this C# console application, follow these steps:

### Prerequisites

Before you begin, make sure you have the following prerequisites installed on your system:

-   [.NET SDK](https://dotnet.microsoft.com/en-us/download) - Ensure you have the .NET SDK installed to build and run C# applications.

### Clone the Repository

If you haven't already, clone this repository to your local machine using Git:

```bash
git clone https://github.com/Yousef-Majidi/Learn2Blog.git
```

### Navigate to the Project Directory

change your working directory to the project folder:

```bash
cd learn2blog
```

### Build the App

Use the following command to build the C# console application:

```bash
dotnet build
```

### Run the App

After the build is complete, navigate to `bin/Debug/net7.0`:

```bash
cd bin/Debug/net7.0
```

You should now have a `Learn2Blog.exe` file in your directory. Run the app by using one of the options or provide an input argument:

```bash
# shows help
./Learn2Blog.exe -h
```

```bash
# converts the input.txt file into html
./Learn2Blog.exe input.txt
```

```bash
# converts all .txt files in the directory into html
./Learn2Blog.exe inputDirectory
```

#### notes on usage (to be added to readme.md properly later):

-   the app will create a default output directory called "til" in the current directory if it doesn't exist. If that directory exists, it will overwrite the files in it.
-   the name of the input files will be used as the title of the html files, unless there is a title specified in the input file. A title is the first line of the input file followed by 2 new lines (\n\n).
-   if no option flag is provided, the app will look for the first argument provided as the input file or directory.
