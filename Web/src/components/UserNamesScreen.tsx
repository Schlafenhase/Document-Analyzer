import { useEffect, useState } from "react";
import styled from "styled-components";
import Button from "./UI/Button";
import Table from "./UI/Table";
import axios from "axios";
import UsersBg from "../assets/UsersBg.jpg";
import { Container } from "./UI/Container";
import { BaseURL } from "../constants";

// const DATA = [
//   {
//     ID: 1,
//     Nombre: "Francisco Solano",
//   },
//   {
//     ID: 2,
//     Nombre: "Pancho Gómez",
//   },
//   {
//     ID: 3,
//     Nombre: "Pepito",
//   },
// ];

const Div = styled.div`
  box-sizing: border-box;
  height: 100%;
  padding: 32px;

  background-image: url(${UsersBg});
  background-size: cover;
`;

const Row = styled.div`
  display: flex;
  justify-content: space-between;
`;

const UserNamesScreen = (props: any) => {
  const [data, setData] = useState([]);
  const [name, setName] = useState("");

  const getEmployees = async () => {
    const token = localStorage.getItem('token');
    const response = await axios.get(BaseURL + "/Api/Employee/Employees", {
      headers: {
        Authorization: "Bearer " + token,
      },
    });
    setData(response.data.employees);
  };

  useEffect(() => {
    getEmployees();
  }, []);

  const postEmployee = async () => {
    const token = localStorage.getItem('token');
    const response = await axios.post(
      BaseURL + "/Api/Employee",
      { name: name },
      {
        headers: {
          Authorization: "Bearer " + token,
        },
      }
    );
    if (response.status === 200) {
      alert("Nombre agregado correctamente");
      setName("");
      setTimeout(getEmployees, 1000);
    } else {
      alert("Error en el servidor");
    }
  };

  return (
    <Div>
      <Container>
        <Row>
          <h1>Nombres de empleados</h1>
          <Button onClick={getEmployees}>Refrescar</Button>
        </Row>
        <Table data={data} />
        <div>
          <input
            placeholder="Nuevo usuario"
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <Button onClick={postEmployee}>Agregar</Button>
        </div>
      </Container>
    </Div>
  );
};

export default UserNamesScreen;
