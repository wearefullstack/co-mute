from asyncio import run_coroutine_threadsafe
from flask import Flask

app = Flask(__name__)
app.config['SECRET_KEY'] = '8123812838123671236as'

@app.route('/')
def test():
    return 'test'




# asdds
if __name__ == "__main__":
    app.run(port=5000, debug=True)
