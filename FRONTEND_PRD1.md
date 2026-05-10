# DVLD Project - Front-End PRD (MVC with Tailwind CSS)

## Document Information

- **Project Name:** Driving License Management System (DVLD)
- **Module:** Front-End MVC Implementation
- **Framework:** ASP.NET MVC with Tailwind CSS
- **Document Version:** 1.0
- **Date:** May 9, 2026

---

## 1. Project Overview

The DVLD (Driving License Management System) Front-End implementation aims to create a modern, responsive, and user-friendly web application using ASP.NET MVC architecture with Tailwind CSS styling. The application manages all aspects of driving license operations including applications, appointments, tests, licenses, and administrative functions.

### Scope

- Responsive web interface for desktop and mobile devices
- User authentication and authorization
- Dashboard and administrative panels
- Data management interfaces (CRUD operations)
- Report generation and viewing
- Real-time status tracking

---

## 2. Design System & Color Palette

### Primary Colors

| Color Name        | Hex Code | RGB Values         | Usage                                          |
| ----------------- | -------- | ------------------ | ---------------------------------------------- |
| **Dark Charcoal** | #222831  | rgb(34, 40, 49)    | Navigation bars, sidebars, main background     |
| **Charcoal Gray** | #393E46  | rgb(57, 62, 70)    | Secondary backgrounds, borders, text           |
| **Teal Accent**   | #00ADB5  | rgb(0, 173, 181)   | Primary CTAs, highlights, interactive elements |
| **Light Gray**    | #EEEEEE  | rgb(238, 238, 238) | Cards, panels, light backgrounds               |

### Color Usage Guidelines

- **Dark Charcoal (#222831):** Header, footer, navigation, main background
- **Charcoal Gray (#393E46):** Secondary navigation, form sections, subtle backgrounds
- **Teal Accent (#00ADB5):** Buttons, links, hover states, progress indicators
- **Light Gray (#EEEEEE):** Card backgrounds, form inputs, content panels

### Typography Color Mapping

- **Primary Text:** #222831 (Dark Charcoal) on light backgrounds
- **Secondary Text:** #393E46 (Charcoal Gray)
- **Light Text:** #EEEEEE (Light Gray) on dark backgrounds
- **Accent Text/Links:** #00ADB5 (Teal)

---

## 3. Architecture & Layout Structure

### Master Layout Components

1. **Navigation Bar**
   - Application logo and branding
   - Primary navigation menu
   - User profile and logout
   - Responsive hamburger menu for mobile

2. **Sidebar Navigation** (Admin/Dashboard views)
   - Module categories
   - Quick access links
   - Collapsible menu items
   - Active state indicators

3. **Main Content Area**
   - Breadcrumb navigation
   - Page header with title and actions
   - Dynamic content region
   - Footer

4. **Footer**
   - Copyright information
   - Quick links
   - Contact information
