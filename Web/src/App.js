import "./App.css";
import LogInScreen from "./Components/LogInScreen";
import { Switch, Route, Redirect, Link, useLocation } from "react-router-dom";
import HomeScreen from "./Components/HomeScreen";
import UserNamesScreen from "./Components/UserNamesScreen";

function App() {
  const location = useLocation();
  console.log(location);
  return (
    <div className="App">
      {location.pathname !== "/" && (
        <div>
          <Link to="/">Cerrar sesión</Link>
          <span> | </span>
          <Link to="/home">Documentos</Link>
          <span> | </span>
          <Link to="/usernames">Nombres de usuarios</Link>
        </div>
      )}
      <Switch>
        <Route path="/" exact>
          <LogInScreen></LogInScreen>
        </Route>
        <Route path="/home">
          <HomeScreen></HomeScreen>
        </Route>
        <Route path="/usernames">
          <UserNamesScreen></UserNamesScreen>
        </Route>
        <Redirect to="/"></Redirect>
      </Switch>
    </div>
  );
}

export default App;
