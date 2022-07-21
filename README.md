# FallingSkies 2021

## Project Overview

This project is developed by [Centre for Digital Media](https://thecdm.ca/) students for [University of British Columbia ALIVE Research Lab](https://alivelab.ca/). You can learn more about this project, including team members and the client, at [the project page](https://thecdm.ca/projects/industry-projects/assessment-inquiry-learning-using-mobile-applications-ubc-alive).

The project contains two parts: an iPad game developed by Unity, and a server developed by Django. This repository contains source code for the Unity game.

## Development Environment

To research on, further develop, or compile a running copy of the game, you need to set up the development environment. The game is developed for iPad and iPad only, so you also need a macOS computer to run Xcode on, an iPad to run the game, and an Apple Developer account ($99/y).

For easy testing, we also provide Windows builds. If you just want to try the game out, the easiest way is to download a pre-compiled Windows build and run it on your PC.

The game is developed with Unity on a Windows platform. Please use the following Unity version to avoid compatibility issues. If Unity Hub no longer provides the said version, you can download it in [Unity download archive](https://unity3d.com/get-unity/download/archive).

* Unity: 2020.3.9f1 (LTS)

To clone this repository and gain full access to its history, you are supposed to set up Git along with Git LFS. Please read [Git for Windows Survival Guide](Documentation/git_for_windows.md) for further information.

If you prefer another version management solution, you can simply download the latest archive from the web interface.

## Building

The game features an access code system that grants access to the player only after a survey is finished. The survey results will be encoded and signed by the game server, and the player will paste the payload into the game. The game will also collect the user metrics and upload the server. Please change the server details in the following files before building:

```
# LoginManager.cs
const string Psk = "";
const string SurveyUrl = "";

# MetricsUploader.cs
const string EventApiUrl = "";
```
