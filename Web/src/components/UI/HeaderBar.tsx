import React from "react";
import {Link} from "react-router-dom";

const HeaderBar = (props: any) => {
    return (
    <div>
        <Link to="/">Cerrar sesión</Link>
        <span> | </span>
        <Link to="/home">Documentos</Link>
        <span> | </span>
        <Link to="/usernames">Nombres de usuarios</Link>
    </div>
    )
};

export default HeaderBar;
