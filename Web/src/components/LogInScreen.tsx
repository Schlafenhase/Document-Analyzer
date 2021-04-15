import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import styled from "styled-components";
import login from "../assets/login.jpg";
import Button from "./UI/Button";
import Input from "./UI/Input";
import axios from "axios";
import { BaseURL } from "../constants";

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

const LogInScreen = (props: any) => {
  const nameState = useState("");
  const passwordState = useState("");
  const history = useHistory();

  const login = async () => {
    try {
      const response = await axios.post(BaseURL + "/Api/AuthManagement/Login", {
        email: nameState[0],
        password: passwordState[0],
      });
      const token = response.data.token;
      props.setToken(token);
      localStorage.setItem("token", token);
      history.push("/home");
      window.location.reload();
    } catch (error) {
      alert("Contraseña o usuario incorrecto");
    }
  };

  return (
    <Div>
      <Container>
        <h1>¡Bienvenido a Document Analyzer!</h1>
        <Input label="Nombre de usuario" state={nameState} />
        <Input type="password" label="Contraseña" state={passwordState} />
        <Button onClick={login}>Ingresar</Button>
      </Container>
    </Div>
  );
};

export default LogInScreen;
