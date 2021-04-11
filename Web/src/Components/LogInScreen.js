import React from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";
import login from "../assets/login.jpg";
import Button from "./UI/Button";
import Input from "./UI/Input";

const Div = styled.div`
  overflow: hidden;
  height: 100%;
  display: flex;
`;

const Container = styled.div`
  background-color: #eef0f2;
  padding: 16px;

  text-align: center;
  justify-content: center;
  display: flex;
  flex-direction: column;

  h1 {
    color: #1c448e;
  }

  input {
    margin-bottom: 16px;
  }
`;

const Img = styled.img`
  width: 100%;
  min-height: 100%;
`;

const LogInScreen = () => {
  return (
    <Div>
      <Img src={login}></Img>
      <Container>
        <h1>¡Bienvenido a Document Analyzer!</h1>
        <Input label="Nombre de usuario"></Input>
        <Input type="password" label="Contraseña"></Input>
        <Link to="/home">
          <Button style={{ width: "100%" }}>Ingresar</Button>
        </Link>
      </Container>
    </Div>
  );
};

export default LogInScreen;
