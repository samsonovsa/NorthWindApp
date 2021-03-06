/*
 * Code generated by Microsoft (R) AutoRest Code Generator.
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

import * as msRest from "@azure/ms-rest-js";
import * as Models from "../models";
import * as Mappers from "../models/categoryApiMappers";
import * as Parameters from "../models/parameters";
import { MyTitleContext } from "../myTitleContext";

/** Class representing a CategoryApi. */
export class CategoryApi {
  private readonly client: MyTitleContext;

  /**
   * Create a CategoryApi.
   * @param {MyTitleContext} client Reference to the service client.
   */
  constructor(client: MyTitleContext) {
    this.client = client;
  }

  /**
   * @param [options] The optional parameters
   * @returns Promise<Models.CategoryApiGetResponse>
   */
  get(options?: msRest.RequestOptionsBase): Promise<Models.CategoryApiGetResponse>;
  /**
   * @param callback The callback
   */
  get(callback: msRest.ServiceCallback<Models.ProblemDetails>): void;
  /**
   * @param options The optional parameters
   * @param callback The callback
   */
  get(options: msRest.RequestOptionsBase, callback: msRest.ServiceCallback<Models.ProblemDetails>): void;
  get(options?: msRest.RequestOptionsBase | msRest.ServiceCallback<Models.ProblemDetails>, callback?: msRest.ServiceCallback<Models.ProblemDetails>): Promise<Models.CategoryApiGetResponse> {
    return this.client.sendOperationRequest(
      {
        options
      },
      getOperationSpec,
      callback) as Promise<Models.CategoryApiGetResponse>;
  }

  /**
   * @param id
   * @param [options] The optional parameters
   * @returns Promise<Models.CategoryApiGet2Response>
   */
  get2(id: number, options?: msRest.RequestOptionsBase): Promise<Models.CategoryApiGet2Response>;
  /**
   * @param id
   * @param callback The callback
   */
  get2(id: number, callback: msRest.ServiceCallback<Models.ProblemDetails>): void;
  /**
   * @param id
   * @param options The optional parameters
   * @param callback The callback
   */
  get2(id: number, options: msRest.RequestOptionsBase, callback: msRest.ServiceCallback<Models.ProblemDetails>): void;
  get2(id: number, options?: msRest.RequestOptionsBase | msRest.ServiceCallback<Models.ProblemDetails>, callback?: msRest.ServiceCallback<Models.ProblemDetails>): Promise<Models.CategoryApiGet2Response> {
    return this.client.sendOperationRequest(
      {
        id,
        options
      },
      get2OperationSpec,
      callback) as Promise<Models.CategoryApiGet2Response>;
  }

  /**
   * @param id
   * @param [options] The optional parameters
   * @returns Promise<Models.CategoryApiGetImageResponse>
   */
  getImage(id: number, options?: msRest.RequestOptionsBase): Promise<Models.CategoryApiGetImageResponse>;
  /**
   * @param id
   * @param callback The callback
   */
  getImage(id: number, callback: msRest.ServiceCallback<Models.ProblemDetails>): void;
  /**
   * @param id
   * @param options The optional parameters
   * @param callback The callback
   */
  getImage(id: number, options: msRest.RequestOptionsBase, callback: msRest.ServiceCallback<Models.ProblemDetails>): void;
  getImage(id: number, options?: msRest.RequestOptionsBase | msRest.ServiceCallback<Models.ProblemDetails>, callback?: msRest.ServiceCallback<Models.ProblemDetails>): Promise<Models.CategoryApiGetImageResponse> {
    return this.client.sendOperationRequest(
      {
        id,
        options
      },
      getImageOperationSpec,
      callback) as Promise<Models.CategoryApiGetImageResponse>;
  }

  /**
   * @param id
   * @param [options] The optional parameters
   * @returns Promise<Models.CategoryApiUpdateImageResponse>
   */
  updateImage(id: number, options?: Models.CategoryApiUpdateImageOptionalParams): Promise<Models.CategoryApiUpdateImageResponse>;
  /**
   * @param id
   * @param callback The callback
   */
  updateImage(id: number, callback: msRest.ServiceCallback<Models.ProblemDetails>): void;
  /**
   * @param id
   * @param options The optional parameters
   * @param callback The callback
   */
  updateImage(id: number, options: Models.CategoryApiUpdateImageOptionalParams, callback: msRest.ServiceCallback<Models.ProblemDetails>): void;
  updateImage(id: number, options?: Models.CategoryApiUpdateImageOptionalParams | msRest.ServiceCallback<Models.ProblemDetails>, callback?: msRest.ServiceCallback<Models.ProblemDetails>): Promise<Models.CategoryApiUpdateImageResponse> {
    return this.client.sendOperationRequest(
      {
        id,
        options
      },
      updateImageOperationSpec,
      callback) as Promise<Models.CategoryApiUpdateImageResponse>;
  }
}

// Operation Specifications
const serializer = new msRest.Serializer(Mappers);
const getOperationSpec: msRest.OperationSpec = {
  httpMethod: "GET",
  path: "api/category",
  responses: {
    404: {
      bodyMapper: Mappers.ProblemDetails
    },
    default: {}
  },
  serializer
};

const get2OperationSpec: msRest.OperationSpec = {
  httpMethod: "GET",
  path: "api/category/{id}",
  urlParameters: [
    Parameters.id
  ],
  responses: {
    404: {
      bodyMapper: Mappers.ProblemDetails
    },
    default: {}
  },
  serializer
};

const getImageOperationSpec: msRest.OperationSpec = {
  httpMethod: "GET",
  path: "api/category/image/{id}",
  urlParameters: [
    Parameters.id
  ],
  responses: {
    404: {
      bodyMapper: Mappers.ProblemDetails
    },
    default: {}
  },
  serializer
};

const updateImageOperationSpec: msRest.OperationSpec = {
  httpMethod: "PUT",
  path: "api/category/image/{id}",
  urlParameters: [
    Parameters.id
  ],
  formDataParameters: [
    Parameters.image
  ],
  contentType: "multipart/form-data",
  responses: {
    404: {
      bodyMapper: Mappers.ProblemDetails
    },
    default: {}
  },
  serializer
};
