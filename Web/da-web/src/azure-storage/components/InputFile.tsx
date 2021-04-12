import React, { useContext, useRef } from 'react';
import { UploadsViewStateContext } from '../contexts/viewStateContext';

const InputFile: React.FC = () => {
    const context = useContext(UploadsViewStateContext);
    const inputFileRef = useRef<HTMLInputElement>(null);

    /**
     * Upload files using Upload Service
     * @param files list of files to upload
     */
    const uploadFiles = (files: FileList | null) => {
        if (files) {
            context.uploadItems(files);
            for (let i = 0; i < files.length; i++) {
                console.log(files[i]);
            }
        }
    }

    return (
    <div className="input-file">
        <input
            ref={inputFileRef}
            type="file"
            accept=".pdf, .docx, .txt"
            multiple={true}
            onChange={e => uploadFiles(e.target.files)}
        />
    </div>
    );
};

export default InputFile;
