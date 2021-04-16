import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import styled from "styled-components";
import Button from "./UI/Button";
import axios from "axios";
import { BaseURL } from "../constants";
import logo from "../assets/da-logo-words-vertical-raster.png";
import signInIcon from "../assets/signIn-icon.svg";
import Entry from "./UI/Entry";
import schlafenhaseLogo from "../assets/schlafenhase-blue-transparent.png";
import gitHubIcon from "../assets/github-icon.svg";

const Div = styled.div`
  overflow: hidden;
  height: 100%;
  display: flex;
  background-size: contain;
  background: linear-gradient(308deg, #48879f, #43c19e, #48879f, #43c19e);
  background-size: 800% 800%;

  -webkit-animation: AnimatedGradientBackground 8s ease infinite;
  -moz-animation: AnimatedGradientBackground 8s ease infinite;
  -o-animation: AnimatedGradientBackground 8s ease infinite;
  animation: AnimatedGradientBackground 8s ease infinite;
  
  @-webkit-keyframes AnimatedGradientBackground {
    0%{background-position:0 50%}
    50%{background-position:100% 51%}
    100%{background-position:0 50%}
  }
  @-moz-keyframes AnimatedGradientBackground {
    0%{background-position:0 50%}
    50%{background-position:100% 51%}
    100%{background-position:0 50%}
  }
  @-o-keyframes AnimatedGradientBackground {
    0%{background-position:0 50%}
    50%{background-position:100% 51%}
    100%{background-position:0 50%}
  }
  @keyframes AnimatedGradientBackground {
    0%{background-position:0 50%}
    50%{background-position:100% 51%}
    100%{background-position:0 50%}
  }
`;

const Container = styled.div`
  margin: 0 auto 0 auto;
  background-color: #ffffff;
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

const Logo = styled.img`
  margin: 0 auto;
  width: 300px;
`;

const InputLabel = styled.div`
  font-size: 13pt;
  margin-top: 20px;
  margin-bottom: 20px;
`;

const BottomLabel = styled.div`
  font-size: 12pt;
  margin-bottom: 10px;
  
  .blue {
    color: blue;
  }
  
  a {
    img {
      height: 30px;
      padding-top: 15px;
      margin-left: 15px;
    }
  }
`;

const HBar = styled.div`
  background-color: #f61067;
  width: 100%;
  height: 10px;
  margin-top: 20px;
  margin-bottom: 0;
  border-radius: 8px;
;`;

const SchlafenhaseLogo = styled.img`
  width: 150px;
  margin: 20px auto 0 auto;
`;

const LogInScreen = (props: any) => {
  const nameState = useState("");
  const passwordState = useState("");
  const history = useHistory();

  /**
   * Authenticates user in API from sign in data
   */
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
      alert("Incorrect username/email or password");
    }
  };

  return (
    <Div>
      <Container>
        <Logo src={logo}/>
        <h2>upload -{">"} analyze -{">"} check.</h2>
        <InputLabel>Welcome back! Sign in here:</InputLabel>
        <Entry label="Username/Email" state={nameState} />
        <Entry type="password" label="Password" state={passwordState} />
        <Button onClick={login} style={{height: 50}}>
          Sign In
          <img src={signInIcon}/>
        </Button>
        <HBar/>
        <SchlafenhaseLogo src={schlafenhaseLogo}/>
        <BottomLabel>2021. Schlafenhase <span className="blue">[BLUE]</span></BottomLabel>
        <BottomLabel>
          Source Code:
          <a href="https://github.com/Schlafenhase/Document-Analyzer">
            <img src={gitHubIcon}/>
          </a>
        </BottomLabel>
      </Container>
    </Div>
  );
};

export default LogInScreen;
