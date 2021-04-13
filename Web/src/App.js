import "./App.css";
import LogInScreen from "./Components/LogInScreen";
import { Switch, Route, Redirect } from "react-router-dom";
import HomeScreen from "./Components/HomeScreen";

function App() {
  return (
    <div className="App">
      <Switch>
        <Route path="/login">
          <LogInScreen></LogInScreen>
        </Route>
        <Route path="/home">
          <HomeScreen></HomeScreen>
        </Route>
        <Redirect to="/login"></Redirect>
      </Switch>
    </div>
  );
}

export default App;
