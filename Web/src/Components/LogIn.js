import React from "react";
import styled from "styled-components";
import login from "../assets/login.jpg";
import Input from "./Input";

const Div = styled.div`
  overflow: hidden;
  height: 100%;
  position: relative;
  display: flex;
`;

const Container = styled.div`
  background-color: #ccc;
  padding: 8px;
  border-radius: 8px;
  z-index: 100;
  position: relative;
  width: 480px;
  margin: auto;
  text-align: center;
  display: flex;
  flex-direction: column;

  h1 {
    margin-bottom: 64px;
  }

  input {
    margin-bottom: 16px;
  }
`;

const Img = styled.img`
  max-width: 100%;
  min-height: 100%;
  position: absolute;
  top: 0;
`;

const LogIn = () => {
  return (
    <Div>
      <Container>
        <h1>¡Bienvenido a Document Analyzer!</h1>
        <Input label="Nombre de usuario"></Input>
        <Input type="password" label="Contraseña"></Input>
        <button>Ingresar</button>
      </Container>
      <Img src={login}></Img>
    </Div>
  );
};

export default LogIn;
