# Snake Game Project

## Overview
The Snake game is a classic arcade game that is simple yet challenging. The player controls a snake that moves around the screen, eating food while avoiding collisions with the walls and itself. The game features increasing difficulty as the snake grows in length after eating food.

## Features
- Simple and intuitive gameplay
- Increasing difficulty levels
- Score tracking
- Option to restart the game

## Technological Stack
- Programming Language: C#
- Game Library: C#
- Architecture: Inversion of Control (IoC)

## Inversion of Control (IoC) Container Architecture
In this project, I employ an Inversion of Control (IoC) architecture to enhance the modularity and testability of the code. The IoC container is responsible for the creation and management of game components, facilitating better separation of concerns. Here's a breakdown of how the IoC principles are applied:

- **Dependency Injection**: Game components such as the Snake, Food, and GameManager are managed through dependency injection to reduce coupling between classes.
- **Service Locator**: A service locator pattern is utilized to retrieve instances of dependent services and classes, promoting flexibility in how these services are used within different parts of the game.

## Getting Started
To run the Snake game:
1. Clone the repository: `git clone https://github.com/Sand82/Snake.git`
2. Navigate to the project directory: `cd Snake`
3. Run the game: `via proper IDE like Visual Studio`

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
