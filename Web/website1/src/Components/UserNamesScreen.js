import React, { useState } from "react";
import styled from "styled-components";
import Button from "./UI/Button";
import Table from "./UI/Table";

const DATA = [
  {
    ID: 1,
    Nombre: "Francisco Solano",
  },
  {
    ID: 2,
    Nombre: "Pancho Gómez",
  },
  {
    ID: 3,
    Nombre: "Pepito",
  },
];

const Div = styled.div`
  display: flex;
  flex-direction: column;
  box-sizing: border-box;
  height: 100%;
  padding: 16px 32px;
`;

const UserNamesScreen = () => {
  const [data, setData] = useState(DATA);
  const [name, setName] = useState("");

  const insert = () => {
    const newUser = { ID: data[data.length - 1].ID + 1, Nombre: name };
    setData([...data, newUser]);
  };

  return (
    <Div>
      <Table data={data}></Table>
      <div>
        <input
          placeholder="Nuevo usuario"
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
        ></input>
        <Button onClick={insert}>Agregar</Button>
      </div>
    </Div>
  );
};

export default UserNamesScreen;
