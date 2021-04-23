import React, { useEffect, useState } from "react";
import "./styles/sass/App.scss";
import {
  Switch,
  Route,
  Redirect,
  BrowserRouter,
  useHistory,
} from "react-router-dom";
import LogInScreen from "./components/LogInScreen";
import HomeScreen from "./components/HomeScreen";
import UserNamesScreen from "./components/UserNamesScreen";
import HeaderBar from "./components/UI/HeaderBar";
import axios from "axios";
import { BaseURL } from "./constants";

const App = () => {
  const [token, setToken]: any = useState();
  const history = useHistory();

  const checkToken = async () => {
    const savedToken = localStorage.getItem("token");
    if (savedToken) {
      const response = await axios.get(BaseURL + "/Api/Employee/Employees", {
        headers: {
          Authorization: "Bearer " + savedToken,
        },
      });
      if (response.data) {
        setToken(savedToken);
        return;
      }
    }
    history.push("/");
  };

  useEffect(() => {
    checkToken();
  }, []);

  return (
    <div className="App">
      <BrowserRouter>
        <Switch>
          <Route path="/" exact>
            <LogInScreen setToken={setToken} />
          </Route>
          <Route path="/home">
            <HeaderBar setToken={setToken} />
            <HomeScreen token={token} />
          </Route>
          <Route path="/usernames">
            <HeaderBar setToken={setToken} />
            <UserNamesScreen token={token} />
          </Route>
          <Redirect to="/" exact />
        </Switch>
      </BrowserRouter>
    </div>
  );
};

export default App;
