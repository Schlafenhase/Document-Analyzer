import React, {useContext, useEffect, useRef, useState} from "react";
import styled from "styled-components";

import ContainerList from "../azure-storage/components/ContainerList";
import SelectedContainer from "../azure-storage/components/SelectedContainer";
import ItemsList from "../azure-storage/components/ItemsList";
import ItemsUploaded from "../azure-storage/components/ItemsUploaded";
import ItemsDownloaded from "../azure-storage/components/ItemsDownloaded";
import ItemsDeleted from "../azure-storage/components/ItemsDeleted";
import { SharedViewStateContext } from "../azure-storage/contexts/viewStateContext";
import { Container } from "./UI/Container";
import {Button, Card, CardContent} from '@material-ui/core';
import axios from "axios";
import { BaseURL } from "../constants";
import Dropzone from "./UI/Dropzone";
import EmployeeResultsTable from "./UI/EmployeeResultsTable";
import RefreshIcon from "@material-ui/icons/Refresh";
import {makeStyles, withStyles} from "@material-ui/core/styles";
import {DataGrid, GridColumns} from "@material-ui/data-grid";
import {HubConnectionBuilder} from "@microsoft/signalr";

const Div = styled.div`
  box-sizing: border-box;
  height: 100%;
  padding: 32px;

  background: rgb(72, 135, 159);
  background: linear-gradient(
    90deg,
    rgba(72, 135, 159, 1) 0%,
    rgba(67, 193, 158, 1) 45%,
    rgba(67, 197, 158, 1) 55%,
    rgba(73, 122, 159, 1) 100%
  );
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

const InfoLabel = styled.p`
  color: white;
`;

const FileLabel = styled.div`
  color: white;
  font-size: 14pt;
  margin-top: 20px;
  margin-bottom: 20px;
  font-style: italic;
`;

const ProcessingLabel = styled.div`
  color: #c2c2c2;
  font-size: 14pt;
  margin-top: 20px;
  margin-bottom: 20px;
  font-weight: bold;
`;

const useStyles = makeStyles({
  root: {
    minWidth: "300px",
    maxWidth: "750px",
    backgroundColor: "#497A9F",
    boxShadow: '0 3px 5px 3px rgba(255, 255, 255, 0.3)',
  }
});

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

const CustomDataGrid = withStyles((theme) => ({
  root: {
    borderRadius: "15px",
    '& .MuiDataGrid-cell': {
      textAlign: "center"
    }
  },
}))(DataGrid);

const columns: GridColumns = [
  { field: 'id', headerName: 'ID', description: 'Employee ID Number', flex: 0.5, headerAlign: 'center' },
  { field: 'name', headerName: 'File Name', description: 'File Name', flex: 0.5, headerAlign: 'center' },
  { field: 'container', headerName: 'Container', description: 'Container Name', flex: 0.5, headerAlign: 'center' }
];

const columnsDetail: GridColumns = [
  { field: 'id', headerName: 'ID', description: 'Employee Name ID', flex: 0.5, headerAlign: 'center', hide: true },
  { field: 'name', headerName: 'Name', description: 'Employee Name', flex: 0.5, headerAlign: 'center' },
  { field: 'count', headerName: 'Count', description: 'Number of times present in file', flex: 0.5, headerAlign: 'center' }
];

const HomeScreen = (props: any) => {
  const [ connection, setConnection ] = useState(null);
  const [data, setData] = useState([]);
  const [fileData, setDataFile]: any = useState({});
  const [status, setStatus]: any = useState();
  const [swAnalysis, setSwAnalysis]: any = useState();
  const [sentAnalysisPer, setSentAnalysisPer]: any = useState();
  const [sentAnalysisMsg, setSentAnalysisMsg]: any = useState();
  const [nameAnalysisDone, setNameAnalysisDone]: any = useState();
  const classes = useStyles();

  const viewContext = useContext(SharedViewStateContext);

  useEffect(() => {
    // Load default Azure Blob container objects
    viewContext.getContainerItems("dcanalyzerblob");
    setStatus("Ready for Analysis");

    // Start connection for web socket
    const newConnection = new HubConnectionBuilder()
        .withUrl(BaseURL + '/hubs/chat')
        .withAutomaticReconnect()
        .build();

    // @ts-ignore
    setConnection(newConnection);
  }, []);

  useEffect(() => {
    // Initially fetch files from server to display on table
    if (props.token) getFiles();
  }, [props.token]);

  useEffect(() => {
    // Detect and reflect changes in database
    if (connection) {
      // @ts-ignore
      connection.start()
          .then((result: any) => {
            console.log('Connected to web socket');

            // Reflect changes
            // @ts-ignore
            connection.on('ReceiveMessage', message => {
              let msg = message.message;
              setStatus(msg);
            });
          })
          .catch((e: any) => console.log('Web socket connection failed: ', e));
    }
  }, [connection]);

  // if (!props.token) {
  //   // window.location.reload();
  //   return null;
  // }

  /**
   * Gets file detail from server
   * @param file File ID to check detail info
   */
  const getDetail = async (file: any) => {
    // Make request in API
    const response = await axios.get(BaseURL + "/Api/File/" + file.id, {
      headers: {
        Authorization: "Bearer " + props.token,
      },
    });

    if (response.data.NameAnalysis.Result != null) {
      setNameAnalysisDone(true);
      // Convert data to table list
      const dataFile = Object.keys(response.data.NameAnalysis.Result).map((key, index) => ({
        id: index,
        name: key,
        count: response.data.NameAnalysis.Result[key],
      })) as any;

      // Set variables to be updated by React
      setSwAnalysis(response.data.SwearAnalysis.Result);
      setSentAnalysisPer(response.data.SentimentAnalysis.ResultPercentage);
      setSentAnalysisMsg(response.data.SentimentAnalysis.ResultMessage);
      setDataFile({title: file.name, data: dataFile});
    } else {
      setNameAnalysisDone(false);
    }
  };

  /**
   * Gets files from server
   */
  const getFiles = async () => {
    console.log(props);
    const response = await axios.get(BaseURL + "/Api/File/Files", {
      headers: {
        Authorization: "Bearer " + props.token,
      },
    });
    setData(response.data);
  };

  /**
   * Status control string method
   */
  function statusControl() {
    setStatus("Uploading to Azure Blob Storage...")
    setTimeout(setStatus("Analyzing File...", 5000))
  }

  /**
   * Upload file to server for document analysis
   * @param name File name to be analyzed
   */
  const uploadFile = async (name: String) => {
    const response = await axios.post(
      BaseURL + "/Api/Analysis",
      {
            name: name,
            container: 'dcanalyzerblob'
            },
      {
        headers: {
          Authorization: "Bearer " + props.token,
        },
      }
    );

    console.log(response.data);
    setTimeout(getFiles, 5000);
    setTimeout(setStatus, 5000);
  };

  return (
    <Div>
      <Container>
        <Card className={classes.root}>
          <CardContent>
            <Container>
              <Row>
                <Title>Saved Files</Title>
                <CustomButton variant="contained" color="primary" onClick={getFiles} style={{height: 50}} startIcon={<RefreshIcon />}>
                  Refresh
                </CustomButton>
              </Row>
              {/*<Table onClickItem={getDetail} data={data} />*/}
              <div style={{ height: 430, width: '100%', backgroundColor: "white", borderRadius: "15px", marginBottom: "20px", marginTop: "20px" }}>
                <CustomDataGrid rows={data} columns={columns} pageSize={6} disableSelectionOnClick onRowClick={(rowData) => getDetail(rowData.row)} />
              </div>
              <FileLabel>System Status:</FileLabel>
              {(status != 'Ready for Analysis') ? (
                <ProcessingLabel>{status}</ProcessingLabel>
              ) : (
                  <ProcessingLabel>Ready for Analysis</ProcessingLabel>
              )}
              <FileLabel>Upload new file:</FileLabel>
              <div>
                { props.token &&
                  <Dropzone
                      start={() => statusControl()}
                      uploaded={uploadFile}
                  />
                }
              </div>
              {fileData.title ? (
                [
                  <FileLabel>Selected File: {fileData.title}</FileLabel>,
                  <FileLabel style={{color:"#f0d980", fontStyle:"normal", fontWeight:"bold"}}>Swear Analysis:</FileLabel>,
                  <ProcessingLabel>{swAnalysis !== -1 ? `There are ${swAnalysis} swear words` : "Swear Analysis Pending..." }</ProcessingLabel>,
                  <FileLabel style={{color:"#f0d980", fontStyle:"normal", fontWeight:"bold"}}>Sentimental Analysis:</FileLabel>,
                  <ProcessingLabel>{swAnalysis !== -1 ? `Positivity is ${sentAnalysisPer}%` : "Sentimental Analysis Pending..."}</ProcessingLabel>,
                  <ProcessingLabel>{swAnalysis !== -1 ? `Content is mostly ${sentAnalysisMsg}` : ""}</ProcessingLabel>,
                  <FileLabel style={{color:"#f0d980", fontStyle:"normal", fontWeight:"bold"}}>Name Analysis:</FileLabel>,
                  // <EmployeeResultsTable data={fileData.data} />,
                  !nameAnalysisDone ? <ProcessingLabel>Name Analysis Pending...</ProcessingLabel> :
                  <div style={{ height: 430, width: '100%', backgroundColor: "white", borderRadius: "15px", marginBottom: "20px", marginTop: "20px" }}>
                    <CustomDataGrid rows={fileData.data} columns={columnsDetail} pageSize={6} disableSelectionOnClick />
                  </div>,
                ]
              ) : (
                <FileLabel>Select a file to view details</FileLabel>
              )}

              <div style={{ display: "none" }}>
                <hr />
                <hr />
                <h2>Debug Components - Delete Them Later:</h2>
                <ContainerList />
                <hr />
                <SelectedContainer className="container" />
                <ItemsList />

                <div className="item-details">
                  <ItemsUploaded />
                  <ItemsDownloaded />
                  <ItemsDeleted />
                </div>
              </div>
            </Container>
          </CardContent>
        </Card>
      </Container>
    </Div>
  );
};

export default HomeScreen;
