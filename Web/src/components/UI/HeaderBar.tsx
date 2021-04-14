import React from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";

const Div = styled.nav`
  background-color: #1c448e;
  color: white;

  display: flex;
  gap: 16px;

  a {
    color: inherit;
  }

  padding: 8px;
`;

const HeaderBar = (props: any) => {
  return (
    <Div>
      <Link to="/">Cerrar sesión</Link>
      <Link to="/home">Documentos</Link>
      <Link to="/usernames">Nombres de usuarios</Link>
    </Div>
  );
};

export default HeaderBar;
