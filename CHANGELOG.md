# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
### Changed
- Automatic shooting when moving the player

### Fixed
- Fix issue where the pause menu wasn't properly working after a restart

## [1.0.1] - 2019-06-13
### Added
- Slowdown the game when player dies

### Changed
- Tweak time between player death and game-over screen (so the player_die sound will finish playing)

### Fixed
- Fix enemy shoot timing, so when the game starts the isn't an immediate enemy shooting sound played
- Fix timing for coroutine, so the speedup timescale doesn't influence the speed_up sound (this wasn't being played correctly)

## [1.0.0] - 2019-04-29
### Added
- Add background music (by RQ)

### Changed
- Fade in background music when starting the game

### Fixed
- Fix scaling of the canvas for proper display in the browser

[Unreleased]: https://github.com/olivierlacan/keep-a-changelog/compare/v1.0.0...HEAD
