import React, { useState } from "react";
import "./styles/sass/App.scss";
import { Switch, Route, Redirect, BrowserRouter } from "react-router-dom";
import LogInScreen from "./components/LogInScreen";
import HomeScreen from "./components/HomeScreen";
import UserNamesScreen from "./components/UserNamesScreen";
import HeaderBar from "./components/UI/HeaderBar";

const App = () => {
  const [baseUrl, setBaseUrl] = useState(
    "http://d436eb62f6a1.ngrok.io/DocAnalyzerApi"
  );
  const [token, setToken] = useState("");

  console.log(baseUrl, token);

  return (
    <div className="App">
      <BrowserRouter>
        <Switch>
          <Route path="/" exact>
            <LogInScreen setBaseUrl={setBaseUrl} setToken={setToken} />
          </Route>
          {(true || token !== "") && [
            <Route path="/home">
              <HeaderBar />
              <HomeScreen baseUrl={baseUrl} token={token} />
            </Route>,
            <Route path="/usernames">
              <HeaderBar />
              <UserNamesScreen baseUrl={baseUrl} token={token} />
            </Route>,
          ]}
          <Redirect to="/" />
        </Switch>
      </BrowserRouter>
    </div>
  );
};

export default App;
