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
      {location.pathname !== "/login" && (
        <div>
          <Link to="/login">Cerrar sesión</Link>
          <span> | </span>
          <Link to="/home">Documentos</Link>
          <span> | </span>
          <Link to="/usernames">Nombres de usuarios</Link>
        </div>
      )}
      <Switch>
        <Route path="/login">
          <LogInScreen></LogInScreen>
        </Route>
        <Route path="/home">
          <HomeScreen></HomeScreen>
        </Route>
        <Route path="/usernames">
          <UserNamesScreen></UserNamesScreen>
        </Route>
        <Redirect to="/login"></Redirect>
      </Switch>
    </div>
  );
}

export default App;
