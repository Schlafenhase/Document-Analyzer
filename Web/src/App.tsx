import React from 'react';
import './styles/sass/App.scss';
import { Switch, Route, Redirect, BrowserRouter } from 'react-router-dom';
import LogInScreen from "./components/LogInScreen";
import HomeScreen from "./components/HomeScreen";
import UserNamesScreen from "./components/UserNamesScreen";
import HeaderBar from "./components/UI/HeaderBar";

class App extends React.Component {

  render() {
    return (
    <div className="App">
      <BrowserRouter>
        <Switch>
          <Route path="/" exact>
            <LogInScreen/>
          </Route>
          <Route path="/home">
            <HeaderBar/>
            <HomeScreen/>
          </Route>
          <Route path="/usernames">
            <HeaderBar/>
            <UserNamesScreen/>
          </Route>
          <Redirect to="/"/>
        </Switch>
      </BrowserRouter>
    </div>
    )
  }
}

export default App;
