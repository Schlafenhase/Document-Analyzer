import React from "react";
import { render, screen } from "@testing-library/react";
import App from "./App";
import LogInScreen from "./components/LogInScreen";
import HomeScreen from "./components/HomeScreen";

test("renders main page", () => {
  render(<App />);
  const linkElement = screen.getByText(/Schlafenhase/i);
  expect(linkElement).toBeInTheDocument();
});

test("renders login", () => {
  render(<LogInScreen />);
  const linkElement = screen.getByText(/Welcome back! Sign in here/i);
  expect(linkElement).toBeInTheDocument();
});

test("renders home", () => {
  render(<HomeScreen token="aaa" />);
  const linkElement = screen.getByText(/Saved Files/i);
  expect(linkElement).toBeInTheDocument();
});
