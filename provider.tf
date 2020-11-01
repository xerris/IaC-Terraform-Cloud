provider "aws" {  
  region = "us-west-2"
}

locals {
  common_variables = {
    AWS_ACCESS_KEY_ID = var.aws_access_key_id
    AWS_SECRET_ACCESS_KEY = var.aws_secret_access_key
    region = var.region
  }
}
