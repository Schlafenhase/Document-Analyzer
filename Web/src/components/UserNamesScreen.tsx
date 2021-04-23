import { useEffect, useState } from "react";
import styled from "styled-components";
import Button from "./UI/Button";
import Table from "./UI/Table";
import axios from "axios";
import UsersBg from "../assets/UsersBg.jpg";
import { Container } from "./UI/Container";
import { BaseURL } from "../constants";
import refreshIcon from "../assets/refresh-icon.svg";
import addIcon from "../assets/add-icon.svg";
import Swal from "sweetalert2";
import Entry from "./UI/Entry";

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

const Title = styled.h1`
  font-size: 24pt;
  margin: 0 10px 10px 10px;
  color: white;
`;

const UserNamesScreen = (props: any) => {
  const [data, setData] = useState([]);
  const [name, setName] = useState("");

  const getEmployees = async () => {
    const response = await axios.get(BaseURL + "/Api/Employee/Employees", {
      headers: {
        Authorization: "Bearer " + props.token,
      },
    });
    setData(response.data.employees);
  };

  useEffect(() => {
    if (props.token) getEmployees();
  }, [props.token]);

  const postEmployee = async () => {
    const response = await axios.post(
      BaseURL + "/Api/Employee",
      { name: name },
      {
        headers: {
          Authorization: "Bearer " + props.token,
        },
      }
    );
    if (response.status === 200) {
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Employee added",
        showConfirmButton: false,
        timer: 1000,
      });
      setName("");
      setTimeout(getEmployees, 1000);
    } else {
      Swal.fire({
        position: "center",
        icon: "error",
        title: "Server error",
        showConfirmButton: false,
        timer: 1000,
      });
    }
  };

  if (!props.token) {
    //window.location.reload();
    return null;
  }

  return (
    <Div>
      <Container>
        <Row>
          <Title>Employees</Title>
          <Button onClick={getEmployees}>
            Refresh
            <img src={refreshIcon} />
          </Button>
        </Row>
        <Table data={data} />
        <div>
          <Entry label="New User" type="text" state={[name, setName]} />
          <Button onClick={postEmployee}>
            Agregar
            <img src={addIcon} />
          </Button>
        </div>
      </Container>
    </Div>
  );
};

export default UserNamesScreen;
