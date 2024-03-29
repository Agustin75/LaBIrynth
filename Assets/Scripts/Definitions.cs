﻿
public enum BicrossSquareState { Empty, Filled, Undecided }
public enum BicrossSize { FiveXFive, TenXTen, FifteenXFifteen, CustomSize }
public enum ControlType { Bicross, Labyrinth, Menu }
public enum HintTypes { EmptyDot, EmptyLine, FilledDot, FilledLine }
public enum InteractableObjectTypes { Enemy, Gate, Obstacle, Rune }
public enum InteractionStepTypes { PopUp, Puzzle, PassedObstacle, Story, UnlockMap }
public enum ItemType { Rune, Upgrade }
public enum SaveDataType { Enemies, Gates, Maps, Obstacles, Runes, Upgrades, Warps, NumTypes }
public enum TileType { Ground, Pit, Wall }
public enum Themes { Default }