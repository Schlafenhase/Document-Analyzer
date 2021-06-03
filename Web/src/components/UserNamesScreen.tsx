import React, { useEffect, useState } from "react";
import styled from "styled-components";
import {Button, Card, CardContent, TextField} from "@material-ui/core";
import {DataGrid, GridColumns} from '@material-ui/data-grid';
import axios from "axios";
import UsersBg from "../assets/UsersBg.jpg";
import { Container } from "./UI/Container";
import { BaseURL } from "../constants";
import Swal from "sweetalert2";
import { makeStyles } from '@material-ui/core/styles';
import RefreshIcon from '@material-ui/icons/Refresh';
import AddCircleIcon from '@material-ui/icons/AddCircle';
import {withStyles} from "@material-ui/core/styles";

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

const CustomButton = withStyles((theme) => ({
  root: {
    border: "2px solid #ffca0a",
    marginLeft: "20px",
    background: 'linear-gradient(45deg, #184f81 30%, #5490bd 90%)',
    boxShadow: '0 3px 5px 2px rgba(255, 105, 135, .3)',
  },
  label: {
    textTransform: "capitalize",
    fontSize: "14pt"
  }
}))(Button);

const StyledTextField = withStyles((theme) => ({
  root: {
    marginBottom: "20px",
    marginTop: "5px",
    "& .MuiFormLabel-root": {
      color: "white"
    },
    '& label.Mui-focused': {
      color: 'white',
    },
    '& .MuiInput-underline:after': {
      borderBottomColor: "#f61067",
    },
    '& .MuiOutlinedInput-root': {
      '& fieldset': {
        borderColor: 'white',
      },
      '&:hover fieldset': {
        borderColor: 'white',
      },
      '&.Mui-focused fieldset': {
        borderColor: "#f61067",
      },
    },
    '& .MuiInputBase-root': {
      color: 'white',
    },
  },
}))(TextField);

const useStyles = makeStyles({
  root: {
    minWidth: "300px",
    maxWidth: "750px",
    backgroundColor: "#497A9F",
    boxShadow: '0 3px 5px 3px rgba(255, 255, 255, 0.3)',
  }
});

const CustomDataGrid = withStyles((theme) => ({
  root: {
    borderRadius: "15px",
    '& .MuiDataGrid-cell': {
      textAlign: "center"
    }
  },
}))(DataGrid);

const columns: GridColumns = [
  { field: 'id', headerName: 'ID', description: 'Employee ID number', flex: 0.5, headerAlign: 'center' },
  { field: 'name', headerName: 'Name', description: 'Employee Name', flex: 0.5, headerAlign: 'center' }
];

const UserNamesScreen = (props: any) => {
  const [data, setData] = useState([]);
  const nameState = useState("");
  const classes = useStyles();

  /**
   * Gets employee data from server
   */
  const getEmployees = async () => {
    const response = await axios.get(BaseURL + "/Api/Employee/Employees", {
      headers: {
        Authorization: "Bearer " + props.token,
      },
    });
    setData(response.data);
    console.log(data);
  };

  useEffect(() => {
    if (props.token) getEmployees();
  }, [props.token]);

  /**
   * Posts employee data in server
   */
  const postEmployee = async () => {
    let empName = nameState[0];
    if (empName !== "") {
      // Post request in API
      const response = await axios.post(
          BaseURL + "/Api/Employee",
          {name: empName},
          {
            headers: {
              Authorization: "Bearer " + props.token,
            },
          }
      );
      if (response.status === 200) {
        // Success
        Swal.fire({
          position: "center",
          icon: "success",
          title: "Employee added",
          showConfirmButton: false,
          timer: 1000,
        });
        // Set input name as empty string
        nameState[1]("");
        setTimeout(getEmployees, 1000);
      } else {
        // Server error. Fire error
        Swal.fire({
          position: "center",
          icon: "error",
          title: "Server Error",
          showConfirmButton: false,
          timer: 1000,
        });
      }
    } else {
      // Name not valid. Fire warning
      Swal.fire({
        position: 'center',
        icon: 'warning',
        title: 'Please insert valid name',
        showConfirmButton: false,
        timer: 1000
      })
    }
  };

  if (!props.token) {
    //window.location.reload();
    return null;
  }

  return (
    <Div>
      <Container>
        <Card className={classes.root}>
          <CardContent>
            <Container>
              <Row>
                <Title>Employees</Title>
                <CustomButton variant="contained" color="primary" onClick={getEmployees} style={{height: 50}} startIcon={<RefreshIcon />}>
                  Refresh
                </CustomButton>
              </Row>
              {/*<Table data={data} />*/}
              <div style={{ height: 430, width: '100%', backgroundColor: "white", borderRadius: "15px", marginBottom: "20px", marginTop: "20px" }}>
                <CustomDataGrid rows={data} columns={columns} pageSize={6} disableSelectionOnClick  />
              </div>
              <div>
                {/*<Entry label="New User" type="text" state={[name, setName]} />*/}
                <StyledTextField id="input-user" type="text" value={nameState[0]} label="New User" variant="outlined" size="small" onChange={(e) => nameState[1](e.target.value)}/>
                <CustomButton variant="contained" color="primary" onClick={postEmployee} style={{height: 50}} startIcon={<AddCircleIcon />}>
                  Add
                </CustomButton>
              </div>
            </Container>
          </CardContent>
        </Card>
      </Container>
    </Div>
  );
};

export default UserNamesScreen;
