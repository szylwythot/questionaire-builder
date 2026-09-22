# 🎯 Project Requirements: Mini Google Forms (MVP)

**Bottom Line:** A simple questionnaire builder to demonstrate full-stack skills (ASP.NET + React). We will start with a 1-day MVP and expand it later! This app will have 3 main screens. We are keeping these requirements brief for now and will dive into the exact details when we start building each page!

---

## 🛠️ The Tech Stack (MVP)
*   **Frontend:** React (Vite) + Material UI (MUI) 
*   **Backend:** ASP.NET Core 8 (Minimal APIs)
*   **Database:** SQLite + Entity Framework (EF) Core

---

## 📄 1. Dashboard Page (The List)
**Goal:** See all your questionnaires in one place.
*   **What it shows:** A clean list of your saved questionnaires.
*   **The Details:** Each row shows the title and a quick summary.
*   **The Actions:** A button to **"Create New"** and a button to **"Open"** existing ones.

---

## 🛠️ 2. Builder Page (Create/Edit)
**Goal:** Make or change a questionnaire.
*   **What it shows:** The editor screen where you build the form.
*   **The Details:** Add a Title and a list of questions.
*   **The Actions:** 
    *   Click to add a **Text** question.
    *   Click to add a **Checkbox** (Multiple Choice) question.
    *   Click **"Save"** to store it in the database.

---

## 👀 3. Viewer Page (Read)
**Goal:** See the final form exactly as a user would see it.
*   **What it shows:** The published, read-only questionnaire. 
*   **The Details:** Just the questions and empty inputs (text boxes and checkboxes). 
*   **The Actions:** (MVP) Just viewing. (Future) A "Submit" button to actually collect answers.

---

## 🚀 Future Enhancements (Post-MVP)
*   **Required Questions:** Ability to mark a question as mandatory.
*   **Dependent Questions:** Show/hide questions based on answers to previous ones.
*   **AI Integration:** Use an LLM to auto-generate a questionnaire or evaluate risks.
