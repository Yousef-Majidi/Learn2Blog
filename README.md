# Learn2Blog

This is an open source command line app to help you quickly convert your text and md notes into html files so they can be easily published on your blog.
With this tool, you can write your notes in plain text or md files and convert them into html files with a single command. This way, you can focus on learning to blog and not worry about the technical details of creating a blog.

## Building and Running the App

To build and run this C# console application, follow these steps:

### Prerequisites

Before you begin, make sure you have the following prerequisites installed on your system:

-   [.NET SDK](https://dotnet.microsoft.com/en-us/download) - Ensure you have the .NET SDK installed to build and run C# applications.

### 1. Clone the Repository

If you haven't already, clone this repository to your local machine using Git:

```bash
git clone https://github.com/Yousef-Majidi/Learn2Blog.git
```

### 2. Navigate to the Project Directory

change your working directory to the project folder:

```bash
cd Learn2Blog
```

### 3. Build the App

Use the following command to build the application:

```bash
dotnet build
```

### 4. Run the App

After the build is complete, navigate to `bin/Debug/net7.0`:

```bash
cd bin/Debug/net7.0
```

You should now have a `Learn2Blog.exe` file in your directory.

## Usage

Run the app by using one of the options or provide an input argument:

```bash
# shows help
./Learn2Blog.exe -h
```

```bash
# shows version
./Learn2Blog.exe -v
```

```bash
# converts the input.txt file into html and outputs in the specified output directory
# can also be used with a directory as the input
./Learn2Blog.exe -o inputSample.txt outputDirectory
```

```bash
# converts the input.txt file into html and outputs in the default directory
./Learn2Blog.exe inputSample.txt
```

```bash
# converts all .txt and md files in the directory into html and outputs in the default directory, unless specified with the -o flag
./Learn2Blog.exe inputSampleDirectory
```

```bash
# converts the input.md file into html and outputs in the default directory
# the -o option can also be used in the same way as above
./Learn2Blog.exe inputSample.md
```


### Notes on Usage:

-   The app will place the output in a directory called `til` by default.
-   If the output directory is specified with the `-o` flag, this directory will be used instead.
-   If the output directory already exists, the app will overwrite the files in it.
-   The name of the output file is the same name as the input file.
-   The title of the html file is the same as the input file name by default, unless a title is specified in the input file.
-   A title in the input file is the first line of the file followed by 2 empty lines:

    ```txt
    This is the title
    <empty line>
    <empty line>
    This is the content of the file
    ```

-   The app will convert the bold markdown syntax into the `<strong>` html tag

## Bug Report

To report a bug simply create a new `issue` on the repository with the following information:

- A short description of the bug as the title of the issue
- A detailed description of the bug in the body
- Steps to reproduce the bug (what were you doing when encountered the bug)
- The expected result of your action versus the produced result

Including as much detail will help me easily replicate the bug and investigate the cause and a solution. 
