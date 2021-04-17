import React, {useCallback, useContext, useRef} from 'react';
import {useDropzone} from 'react-dropzone';
import styled from 'styled-components';
import {UploadsViewStateContext} from "../../azure-storage/contexts/viewStateContext";
import Swal from "sweetalert2"

const getColor = (props: any) => {
    if (props.isDragAccept) {
        return '#00e676';
    }
    if (props.isDragReject) {
        return '#ff1744';
    }
    if (props.isDragActive) {
        return '#2196f3';
    }
    return '#eeeeee';
}

const Container = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
  border-width: 2px;
  border-radius: 2px;
  border-color: ${props => getColor(props)};
  border-style: dashed;
  background-color: #fafafa;
  color: #bdbdbd;
  outline: none;
  transition: border .24s ease-in-out;
`;

const Dropzone: any = (props: any) => {
    const context = useContext(UploadsViewStateContext);

    const onDrop = useCallback(acceptedFiles => {
        if (acceptedFiles.length > 0) {
            uploadFiles(acceptedFiles);
        } else {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'File type not supported',
                showConfirmButton: false,
                timer: 1000
            })
        }
    }, [])

    const {
        getRootProps,
        getInputProps,
        isDragActive,
        isDragAccept,
        isDragReject
    } = useDropzone({accept: 'application/pdf,' +
                                    ' text/plain,' +
                                    ' application/vnd.openxmlformats-officedocument.wordprocessingml.document',
                            maxFiles: 1,
                            onDrop: onDrop});

    /**
     * Upload files using Upload Service
     * @param files list of files to upload
     */
    const uploadFiles = (files: FileList | null) => {
        props.start();
        if (files) {
            context.uploadItems(files);
            for (let i = 0; i < files.length; i++) {
                console.log(files[i]);
                setTimeout(() => props.uploaded(files[i].name), 3000);
            }
        }
    };

    return (
        <div className="container">
            <Container {...getRootProps({isDragActive, isDragAccept, isDragReject})}>
                <input {...getInputProps()} />
                <p>Drag 'n' drop some files here, or click to select files</p>
                <p>Supported formats: .pdf, .txt & .docx</p>
            </Container>
        </div>
    );
}

export default Dropzone;
