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
  background-image: url(${login});
  background-size: contain;
  background-position: left;
`;

const Container = styled.div`
  margin-left: auto;
  background-color: #eef0f2;
  padding: 16px;
  width: 320px;

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

const LogInScreen = () => {
    return (
        <Div>
            <Container>
                <h1>¡Bienvenido a Document Analyzer!</h1>
                <Input label="Nombre de usuario"/>
                <Input type="password" label="Contraseña"/>
                <Link to="/home">
                    <Button style={{ width: "100%" }}>Ingresar</Button>
                </Link>
            </Container>
        </Div>
    );
};

export default LogInScreen;
