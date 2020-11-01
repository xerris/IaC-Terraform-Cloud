provider "aws" {  
  region: "us-west-2"
}

resource "aws_s3_bucket" "mys3" {
  bucket = "xerris-terraform-s3"
}