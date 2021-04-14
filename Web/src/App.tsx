import React, { useState } from "react";
import "./styles/sass/App.scss";
import { Switch, Route, Redirect, BrowserRouter } from "react-router-dom";
import LogInScreen from "./components/LogInScreen";
import HomeScreen from "./components/HomeScreen";
import UserNamesScreen from "./components/UserNamesScreen";
import HeaderBar from "./components/UI/HeaderBar";

const App = () => {
  const [token, setToken] = useState("");

  return (
    <div className="App">
      <BrowserRouter>
        <Switch>
          <Route path="/" exact>
            <LogInScreen setToken={setToken} />
          </Route>
          {(true || token !== "") && [
            <Route path="/home">
              <HeaderBar />
              <HomeScreen token={token} />
            </Route>,
            <Route path="/usernames">
              <HeaderBar />
              <UserNamesScreen token={token} />
            </Route>,
          ]}
          <Redirect to="/" />
        </Switch>
      </BrowserRouter>
    </div>
  );
};

export default App;
