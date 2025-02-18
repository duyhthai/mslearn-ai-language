import unittest
from unittest.mock import patch
from your_module import main  # Replace 'your_module' with the actual module name

class TestMain(unittest.TestCase):

    @patch.dict(os.environ, {"KEY": "value"})
    def test_main_no_exception(self):
        # Arrange
        load_dotenv()
        
        # Act
        main()
        
        # Assert
        pass  # Add specific assertions if needed

    @patch.dict(os.environ, {})
    def test_main_missing_env_var_exception(self):
        # Arrange
        with self.assertRaises(Exception) as context:
            main()
        
        # Assert
        self.assertIn("Missing environment variable", str(context.exception))

    @patch('dotenv.load_dotenv', side_effect=Exception('Test exception'))
    def test_main_load_dotenv_exception(self):
        # Arrange
        
        # Act
        with self.assertRaises(Exception) as context:
            main()
        
        # Assert
        self.assertEqual(str(context.exception), 'Test exception')

if __name__ == '__main__':
    unittest.main()