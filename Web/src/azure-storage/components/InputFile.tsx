import React, { useContext, useRef } from "react";
import { UploadsViewStateContext } from "../contexts/viewStateContext";

const InputFile: any = (props: any) => {
  const context = useContext(UploadsViewStateContext);
  const inputFileRef = useRef<HTMLInputElement>(null);

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
    <div className="input-file" style={{color: "white"}}>
      <input
        ref={inputFileRef}
        type="file"
        accept=".pdf, .docx, .txt"
        multiple={true}
        onChange={(e) => uploadFiles(e.target.files)}
      />
    </div>
  );
};

export default InputFile;
