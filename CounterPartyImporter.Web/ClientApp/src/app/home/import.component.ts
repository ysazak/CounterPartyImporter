import { Component } from '@angular/core';
import { HttpEventType } from '@angular/common/http';
import { CompanyService } from '../services/company.service';

@Component({
    selector: 'app-import',
    templateUrl: './import.component.html',
})
export class ImportComponent {
    loading = false;
    public progress: number;
    public errorMessage: string;
    public progressStatus: string;
    public isSucessful = false;


    constructor(private companyService: CompanyService) { }

    resetMessages() {
        this.isSucessful = false;
        this.loading = false;
        this.errorMessage = '';
        this.progress = 0;
    }

    onFileChange($event) {
        this.resetMessages();
    }

    upload(files) {
        if (files.length === 0)
            return;

        const formData = new FormData();

        formData.append(files[0].name, files[0]);

        this.loading = true;
        this.companyService.importFile(formData).subscribe(event => {
            switch (event.type) {
                case HttpEventType.Sent:
                    console.log('Sending the file');
                    break;
                case HttpEventType.UploadProgress:
                    this.progress = Math.round(100 * event.loaded / event.total);
                    if (this.progress >= 100) {
                        this.progressStatus = 'Importing Counter Parties...';
                    } else if (this.progress < 100) {
                        this.progressStatus = 'Uploading the file';
                    }
                    break;
                case HttpEventType.ResponseHeader:
                    this.progressStatus = 'File has been processed. Loading the result';
                    break;
                case HttpEventType.DownloadProgress:
                    break;
                case HttpEventType.Response:
                    this.progressStatus = null;
                    this.isSucessful = true;
                    // this.message = event.body.toString();
                    this.loading = false;
                    break;
            }
        },
            err => {
                this.loading = false;
                this.isSucessful = false;
                this.errorMessage = `Error: ${err.error.error}`;
            });


        // const uploadReq = new HttpRequest('POST', `api/CompanyImport/UploadFile`, formData, {
        //     reportProgress: true,
        // });

        // this.http.request(uploadReq).subscribe(event => {
        //     console.log(event.type);
        //     console.log(event);
        //     if (event.type === HttpEventType.UploadProgress) {
        //         this.progress = Math.round(100 * event.loaded / event.total);
        //         if (this.progress >= 100) {
        //             this.progressStatus = 'Importing Counter Parties...';
        //         }
        //         else if (this.progress < 100) {
        //             this.progressStatus = 'Uploading the file';
        //         }
        //     }
        //     else if (event.type === HttpEventType.ResponseHeader) {
        //         this.progressStatus = 'File has been processed. Loading the result';
        //     }
        //     else if (event.type === HttpEventType.Response) {
        //         this.progressStatus = null;
        //         this.isSucessful = true;
        //         // this.message = event.body.toString();
        //         this.loading = false;
        //     }
        // },
        //     err => {
        //         this.loading = false;
        //         this.isSucessful = false;
        //         this.errorMessage = `Error: ${err.error.error}`;
        //     });
    }
}