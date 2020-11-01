

resource "aws_dynamodb_table" "tfc_example_table" {
  name = "${var.db_table_name}"

  read_capacity  = var.db_read_capacity
  write_capacity = var.db_write_capacity
  hash_key       = "Id"
  removal_policy = "DESTROY"

  attribute {
    name = "Id"
    type = "S"
  }
}