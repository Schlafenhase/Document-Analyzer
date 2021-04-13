import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders app name in main page', () => {
  render(<App />);
  const linkElement = screen.getByText(/Document Analyzer/i);
  expect(linkElement).toBeInTheDocument();
});
