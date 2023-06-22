import React from "react";
import "../globals/navbar.css";

const navItems: {
  title: string;
  path: string;
}[] = [
  {
    title: "Home",
    path: "https://localhost:7092/",
  },
  {
    title: "Studio",
    path: "/studio",
  },
  {
    title: "Chat",
    path: "/chat",
  },
];

export const Navbar: React.FC = () => {
  return (
    <header>
      <div className="logo-container">
        <img src="/img/tcs.png" />
      </div>
      <nav>
        <ul>
          {navItems.map(({ title, path }) => (
            <li key={title}>
              <a href={path}>{title}</a>
            </li>
          ))}
        </ul>
      </nav>

      <form action="https://localhost:7092/api/Auth/Logout" method="get">
        <button type="submit">Logout</button>
      </form>
      <div className="profile-container">
        <img src="https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png" />
      </div>
    </header>
  );
};
