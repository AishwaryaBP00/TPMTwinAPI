/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { SprintCandidateSummaryDto } from '../../models/sprint-candidate-summary-dto';

export interface ApiSprintCandidatesSummaryGet$Plain$Params {
}

export function apiSprintCandidatesSummaryGet$Plain(http: HttpClient, rootUrl: string, params?: ApiSprintCandidatesSummaryGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<SprintCandidateSummaryDto>>> {
  const rb = new RequestBuilder(rootUrl, apiSprintCandidatesSummaryGet$Plain.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<SprintCandidateSummaryDto>>;
    })
  );
}

apiSprintCandidatesSummaryGet$Plain.PATH = '/api/SprintCandidates/summary';
