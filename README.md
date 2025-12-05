<img width="150" height="150" alt="pearz-rounded" src="https://github.com/user-attachments/assets/bb8e74a9-8f37-45e2-9e34-f39360e85a36" />

# 🍐 Pearz - A Game of Pairs

A lightweight card-matching puzzle game built with Unity, designed for **iOS** and **macOS**.  
The project uses clean architecture, robust saving/loading, and scalable gameplay systems.

---

## 🛠 Unity Version

- Unity **2021.3.45f2 LTS**
- Platforms: **macOS, iOS** || *should work on Windows & Android too, but not tested*

---

## 🚩 Game Overview

Match pairs of cards to earn points.  
Correct matches grow combos, incorrect pairs reset them.
Progress is auto-saved so the game can be **continued after closing**.

---

## ✨ Features

- 🔢 3 Difficulty Modes (Easy / Medium / Hard)  
- 🔄 Card Flip Animation  
- 🧠 Stateful Comparison Engine (continuous clicking behavior)  
- 🔐 Save/Load Persistent Progress  
- 🔈 SFX Feedback (match, mismatch, flip, win)  
- 📱 UI auto-scales for iOS and macOS  
- 🎨 Random sprite assignment to pairs  
- 💪 Combo Scoring System  
- ♻️ Restart and Level Select
- 📦 Zero third-party assets and frameworks

---

## 🧩 Main System Responsibilities

### GameManager
- Core state owner (score, progress, combo)
- Handles game Start, Reset, Win, and Level Select Scenarios
- Handles saving/loading using SaveManager
- Coordinates UI responses

### DeckManager
- Generates, shuffles, and restores cards
- Bridges data models and visual prefabs
- Assigns sprites randomly

### ComparisonEngine
- Validates selected cards
- Handles match/mismatch rules
- Supports continuous clicking without blocking UI

### UIController
- Routes button actions to GameManager
- Shows/hides panels and screens

### SaveManager
- Prepares data to be saved
- Reconstructs models when loading

### SaveSystem
- Responsible for JSON read/write and deletion

### AudioManager
- One-stop point for SFX playback

---

## 🔁 Save/Load Behavior

### Automatically stored:
- Score  
- Combo  
- Grid  
- Matched cards  
- Sprites  

### Restore behavior:
- Cards return to same position with same sprite  
- UI updates immediately  
- Progress resumes precisely  

### Continue behavior:
- Appears only when a save exists  

---

## 🔊 Audio Events

- Card flip  
- Card match  
- Card mismatch  
- Win celebration  

---

## 🧮 Score System + Combos

- Points are awarded for successful matches  
- Score penalties are applied for incorrect matches  
- A combo system increases points for consecutive correct matches

---

## 🧱 Grid Configuration

| Difficulty | Grid | Cards | Pairs |
|-----------:|------|-------|-------|
| Easy       | 2×2  | 4     | 2     |
| Medium     | 3×4  | 12    | 6     |
| Hard       | 4×4  | 16    | 8     |

Larger grids are supported, requires additional sprites.

---

## 🖼 Sprite Assignment

- Sprites assigned via Inspector to DeckManager
- Randomized per new game
- Same sprite used for matching pair
- SaveSystem stores **sprite name**
- Load restores exact visuals

---

## 🧪 Testing Focus

- Continuous clicking validation  
- Locked card behavior  
- Accurate flip restore  
- Save/load correctness  
- Scaling behavior across devices  

---

## 📦 Future Improvements

- Animations for match and mismatch
- Grid constraints to avoid odd number of cards
- Better Visuals
- Pause Menu and Quit Button
- Add fallback with random colors if number of available sprites is less than grid pairs
