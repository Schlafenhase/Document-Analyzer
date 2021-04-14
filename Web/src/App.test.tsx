import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders learn main page name', () => {
  render(<App />);
  const linkElement = screen.getByText(/Document Analyzer/i);
  expect(linkElement).toBeInTheDocument();
});
