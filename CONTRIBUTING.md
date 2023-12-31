# Contributing to Learn2Blog

First off, thanks for taking the time to contribute! ❤️

All types of contributions are encouraged and valued. See the [Table of Contents](#table-of-contents) for different ways to help and details about how this project handles them. Please make sure to read the relevant section before making your contribution. It will make it a lot easier for us maintainers and smooth out the experience for all involved. The community looks forward to your contributions. 🎉

> And if you like the project, but just don't have time to contribute, that's fine. There are other easy ways to support the project and show your appreciation, which we would also be very happy about:
>
> - Star the project
> - Tweet about it
> - Refer this project in your project's readme
> - Mention the project at local meetups and tell your friends/colleagues

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [I Have a Question](#i-have-a-question)
- [I Want To Contribute](#i-want-to-contribute)
  - [Reporting Bugs](#reporting-bugs)
  - [Suggesting Enhancements](#suggesting-enhancements)
  - [Your First Code Contribution](#your-first-code-contribution)
  - [Improving The Documentation](#improving-the-documentation)
- [Styleguides](#styleguides)
  - [Commit Messages](#commit-messages)
- [Join The Project Team](#join-the-project-team)

## Code of Conduct

This project and everyone participating in it is governed by the
[Learn2Blog Code of Conduct](https://github.com/Yousef-Majidi/Learn2Blog/blob/main/CODE_OF_CONDUCT.md).
By participating, you are expected to uphold this code. Please report unacceptable behavior
to <y.majidin@gmail.com>.

## I Have a Question

Before you ask a question, it is best to search for existing [Issues](https://github.com/Yousef-Majidi/Learn2Blog/issues) that might help you. In case you have found a suitable issue and still need clarification, you can write your question in this issue. It is also advisable to search the internet for answers first.

If you then still feel the need to ask a question and need clarification, we recommend the following:

- Open an [Issue](https://github.com/Yousef-Majidi/Learn2Blog/issues/new).
- Provide as much context as you can about what you're running into.
- Provide project and platform versions, depending on what seems relevant.

We will then take care of the issue as soon as possible.

## I Want To Contribute

> ### Legal Notice
>
> When contributing to this project, you must agree that you have authored 100% of the content, that you have the necessary rights to the content and that the content you contribute may be provided under the project license.

### Reporting Bugs

#### Before Submitting a Bug Report

A good bug report shouldn't leave others needing to chase you up for more information. So, we'd like to ask you to investigate carefully, collect data and describe the issue in detail in your report. Please complete the following steps in advance to help us fix any potential bug as fast as possible.

- Make sure that you are using the latest version.
- Determine if your bug is a bug and not an error on your side e.g. using incompatible environment components/versions. If you are looking for support, you might want to check [this section](#i-have-a-question)).
- To see if other users have experienced (and potentially already solved) the same issue you are having, check if there is not already a bug report existing for your bug or error in the [bug tracker](https://github.com/Yousef-Majidi/Learn2Blog/issues?q=label%3Abug).
- Also make sure to search the internet (including Stack Overflow) to see if users outside of the GitHub community have discussed the issue.
- Collect information about the bug:
  - Stack trace (Traceback)
  - OS, Platform and Version (Windows, Linux, macOS, x86, ARM)
  - Version of the interpreter, compiler, SDK, runtime environment, and package manager, depending on what seems relevant.
  - Possibly your input and the output
  - Can you reliably reproduce the issue? And can you also reproduce it with older versions?

#### How Do I Submit a Good Bug Report?

> You must never report security-related issues, vulnerabilities or bugs including sensitive information to the issue tracker, or elsewhere in public. Instead sensitive bugs must be sent by email to <y.majidin@gmail.com>.

We use GitHub issues to track bugs and errors. If you run into an issue with the project:

- Open an [Issue](https://github.com/Yousef-Majidi/Learn2Blog/issues/new). (Since we can't be sure at this point whether it is a bug or not, we ask you not to talk about a bug yet and not to label the issue.)
- Explain the behaviour you would expect and the actual behaviour.
- Please provide as much context as possible and describe the _reproduction steps_ that someone else can follow to recreate the issue on their own. This usually includes your code. For good bug reports you should isolate the problem and create a reduced test case.
- Provide the information you collected in the previous section.

Once it's filed:

- The project team will label the issue accordingly.
- A team member will try to reproduce the issue with your provided steps. If there are no reproduction steps or no obvious way to reproduce the issue, the team will ask you for those steps and mark the issue as `needs-repro`. Bugs with the `needs-repro` tag will not be addressed until they are reproduced.
- If the team is able to reproduce the issue, it will be marked `needs-fix`, as well as possibly other tags (such as `critical`), and the issue will be left to be [implemented by someone](#your-first-code-contribution).

### Suggesting Enhancements

This section guides you through submitting an enhancement suggestion for Learn2Blog, **including completely new features and minor improvements to existing functionality**. Following these guidelines will help maintainers and the community to understand your suggestions and find related suggestions.

#### Before Submitting an Enhancement

- Make sure that you are using the latest version.
- Read the [documentation](https://github.com/Yousef-Majidi/Learn2Blog/blob/main/README.md) carefully and find out if the functionality is already covered, maybe by an individual configuration.
- Perform a [search](https://github.com/Yousef-Majidi/Learn2Blog/issues) to see if the enhancement has already been suggested. If it has, comment on the existing issue instead of opening a new one.
- Find out whether your idea fits with the scope and aims of the project. It's up to you to make a strong case to convince the project's developers of the merits of this feature. Keep in mind that we want features that will be useful to the majority of our users and not just a small subset. If you're just targeting a minority of users, consider writing an add-on/plugin library.

#### How Do I Submit a Good Enhancement Suggestion?

Enhancement suggestions are tracked as [GitHub issues](https://github.com/Yousef-Majidi/Learn2Blog/issues).

- Use a **clear and descriptive title** for the issue to identify the suggestion.
- Provide a **step-by-step description of the suggested enhancement** in as many details as possible.
- **Describe the current behaviour** and **explain which behaviour you expected to see instead** and why. At this point, you can also tell which alternatives do not work for you.
- **Explain why this enhancement would be useful** to most Learn2Blog users. You may also want to point out the other projects that solved it better and which could serve as inspiration.

### Your First Code Contribution

#### Prerequisites

Before you begin, make sure you have the following prerequisites installed on your system:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download) - Ensure you have the .NET SDK installed to build and run C# applications.

#### 1. Clone the Repository

If you haven't already, clone this repository to your local machine using Git:

```bash
git clone https://github.com/Yousef-Majidi/Learn2Blog.git
```

#### 2. Navigate to the Project Directory

change your working directory to the project folder:

```bash
cd Learn2Blog
```

#### 3. Build the App

Use the following command to build the application:

```bash
dotnet build
```

#### 4. Confirm the Build

After the build is complete, navigate to `bin/Debug/net7.0`:

```bash
cd bin/Debug/net7.0
```

You should now have a `Learn2Blog.exe` file in your directory.

> See the [documentation](https://github.com/Yousef-Majidi/Learn2Blog/blob/main/README.md) for more information about how to use the application.

#### 5. Create a Topic Branch

Create a topic branch from where you want to base your work. This is usually the main branch. The branch name usually reflects the issue number or the feature name.

```bash
git checkout -b <topic-branch-name>
```

#### 6. Make Changes

Make whatever changes related to the issue you are working on.

#### 7. Test Your Code

Change directory to the `tests` folder and run the test suite:

```bash
cd tests
dotnet test
```

#### 8. Format Your Code

If you are using VS Code, you can run the `Format` [task](https://code.visualstudio.com/docs/editor/tasks) to automatically format your code by following the steps below:

- Open the command palette using `Ctrl + Shift + P` or `Cmd + Shift + P`.
- Type `Run Task` and select it.
- Select `Format` from the list of tasks.

Alternatively, you can run the following command in the terminal:

```bash
dotnet format Learn2Block.csproj --verbosity n
```

> See `dotnet format -h` for more formatting options.

#### 9. Analyze Your Code

Running the Formatter will also run the code analyzer and fix any potential issues it can find. However, some issues will not automatically fix. You can read the produced log to find them. Alternatively, the code analyzer will run upon build and will print the log to the console when it finds any errors.

#### 10. Commit Changes

Commit your changes using a descriptive commit message.

#### 11. Push Changes

Push your changes to the remote repository on GitHub:

```bash
git push origin <topic-branch-name>
```

#### 12. Submit a Pull Request

Go to the [Pull Requests](https://github.com/Yousef-Majidi/Learn2Blog/pulls) tab on GitHub and click the "New pull request" button. Select the branch you made changes to and fill out the form. Pull requests are reviewed by the project maintainers.

## Attribution

This guide is based on the **contributing-gen**. [Make your own](https://github.com/bttger/contributing-gen)!
