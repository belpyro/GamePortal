import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.scss']
})
export class NotFoundComponent implements OnInit {

  access_token: string;

  constructor(private oauth: OAuthService) { }

  ngOnInit(): void {
    if (this.oauth.hasValidAccessToken())
    {
      console.log('valid token');
      this.access_token = this.oauth.getAccessToken();
    }
  }

}
