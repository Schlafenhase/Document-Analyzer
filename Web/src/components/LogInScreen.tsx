import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import styled from "styled-components";
import {Button, TextField} from "@material-ui/core";
import { makeStyles, withStyles } from '@material-ui/core/styles';
import axios from "axios";
import {AuthURL, BaseURL} from "../constants";
import logo from "../assets/da-logo-words-vertical-raster.png";
import schlafenhaseLogo from "../assets/schlafenhase-blue-transparent.png";
import gitHubIcon from "../assets/github-icon.svg";
import Swal from "sweetalert2";
import ArrowForwardIcon from '@material-ui/icons/ArrowForward';

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

const CustomButton = withStyles((theme) => ({
  root: {
    background: 'linear-gradient(45deg, #184f81 30%, #5490bd 90%)',
    boxShadow: '0 3px 5px 2px rgba(30, 30, 30, .3)',
  },
  label: {
    textTransform: "capitalize",
    fontSize: "14pt"
  }
}))(Button);

const useStyles = makeStyles((theme) => ({
  margin: {
    margin: theme.spacing(1),
  },
}));

const StyledTextField = withStyles((theme) => ({
  root: {
    marginBottom: "20px",
    "& .MuiFormLabel-root": {
      color: theme.palette.secondary.main
    }
  }
}))(TextField);

const LogInScreen = (props: any) => {
  const nameState = useState("");
  const passwordState = useState("");
  const history = useHistory();

  /**
   * Handles key events
   */
  function handleKeyEvent(event: any)  {
    if (event.charCode === 13) {
      // Enter key. Sign In
      login();
    }
  }

  /**
   * Authenticates user in API from sign in data
   */
  const login = async () => {
    try {
      // Define parameters
      const params = new URLSearchParams()
      params.append('username', nameState[0])
      params.append('password', passwordState[0])
      params.append('client_id', 'document-analyzer')
      params.append('grant_type', 'password')

      const config = {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded'
        }
      }
      const response = await axios.post(AuthURL + "/auth/realms/document-analyzer-realm/protocol/openid-connect/token", params, config);
      const token = response.data.access_token;
      props.setToken(token);
      localStorage.setItem("token", token);
      history.push("/home");
      window.location.reload();
    } catch (error) {
      Swal.fire({
        position: 'center',
        icon: 'error',
        title: 'Incorrect username/email or password',
        showConfirmButton: false,
        timer: 1000
      })
    }
  };

  return (
    <Div>
      <Container>
        <Logo src={logo}/>
        <h2>upload -{">"} analyze -{">"} check.</h2>
        <InputLabel>Welcome back! Sign in here:</InputLabel>
        <StyledTextField id="input-email" type="text" label="Email" variant="outlined" size="small" onChange={(e) => nameState[1](e.target.value)}/>
        <StyledTextField id="input-password" type="password" label="Password" variant="outlined" size="small" onChange={(e) => passwordState[1](e.target.value)} onKeyPress={handleKeyEvent} />
        {/*<Entry label="Username/Email" state={nameState} />*/}
        {/*<Entry type="password" label="Password" state={passwordState} />*/}
        <CustomButton variant="contained" color="primary" onClick={login} style={{height: 50}} startIcon={<ArrowForwardIcon />}>
          Sign In
        </CustomButton>
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
