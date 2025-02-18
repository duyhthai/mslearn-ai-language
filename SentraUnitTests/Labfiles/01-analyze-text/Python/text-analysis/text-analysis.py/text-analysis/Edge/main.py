import unittest
from unittest.mock import patch
from io import StringIO
import sys

# Mocking the environment variables
@patch.dict(os.environ, {'KEY': 'value'})
class TestMain(unittest.TestCase):

    def test_main_with_environment_variable(self):
        # Arrange
        expected_output = "Environment variable KEY is set to value\n"

        # Redirect stdout to capture print statements
        original_stdout = sys.stdout
        sys.stdout = captured_output = StringIO()

        # Act
        from your_module_name import main  # Replace 'your_module_name' with actual module name
        main()

        # Assert
        self.assertEqual(captured_output.getvalue(), expected_output)
        sys.stdout = original_stdout

    @patch.dict(os.environ, {})
    def test_main_without_environment_variable(self):
        # Arrange
        expected_output = "Environment variable KEY is not set\n"

        # Redirect stdout to capture print statements
        original_stdout = sys.stdout
        sys.stdout = captured_output = StringIO()

        # Act
        from your_module_name import main  # Replace 'your_module_name' with actual module name
        main()

        # Assert
        self.assertEqual(captured_output.getvalue(), expected_output)
        sys.stdout = original_stdout

if __name__ == '__main__':
    unittest.main()