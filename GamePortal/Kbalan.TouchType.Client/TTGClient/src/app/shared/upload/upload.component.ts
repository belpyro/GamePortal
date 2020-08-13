import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  public progress: number;
  public message: string;
  // tslint:disable-next-line: no-output-on-prefix
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    const fileToUpload = files[0] as File;
    const extn = fileToUpload.name.split('.').pop();
    if (extn === 'svg' || extn === 'png' || extn === 'jpg' || extn === 'jpeg' )
    {

    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post(`${environment.backendurl}/api/upload`, formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        }
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
          if (event.status === 200)
          {
            this.toastr.success('Avatar succsesfully changed');
          }
        }
      },
      err => {
        if ( err != null )
        {
          this.toastr.error(err.error.Message);
        }
      });
  }else{
    this.toastr.error('Incorrect file extension(only jpeg, jpg, png or svg are applied');
  }
}
}
