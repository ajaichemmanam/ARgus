import os

from flask import Flask,jsonify,render_template,redirect
from flask import request
from flask_cors import CORS
import base64
from hackathon import analyseimage

app = Flask(__name__, static_url_path='/static')
CORS(app)


from PIL import Image

extra_files = []

#dateToday = datetime.today().strftime('%Y-%m-%d')

@app.route("/")
def hello():
    message = "Hello, World"
    #return app.send_static_file('myfig.png')
    #return redirect("http://192.168.42.169:5000/static/images/myfig.png", code=302)
    # return render_template('index.html', message=message)
    return "It's Working"

#Images are send to this method for analytics
@app.route('/postjson', methods = ['POST'])
def postJsonHandler():
    if(request.is_json):
        content = request.get_json()
        print(content)
        img_name = content['filename']
        img_string = content['image']
        imgdata = base64.decodebytes(img_string.encode())
        # 
        save_dir = os.path.join(os.getcwd(), "static")
        if not os.path.exists(save_dir):
            os.makedirs(save_dir)
            
        with open(os.path.join(save_dir,img_name),'wb') as f:
            f.write(imgdata)

        image = Image.open(os.path.join(save_dir,img_name))
        analysedData = analyseimage(os.path.join("./static/"+img_name))  
        # image_name="doe.jpeg"
        # image_path= os.path.join("/Users/ajaichemmanam/Documents/Projects/tf/"+image_name)
        
        # return jsonify(json.dumps(str(results)))
        return jsonify(analysedData)
    else:
        print("unsupported request format")
        return "method not allowed"



if __name__ == '__main__':
    
    # app.config['TEMPLATE_AUTO_RELOAD'] = True
    app.run(host='0.0.0.0',port=5000,debug = True)
    # http_server = WSGIServer(('', 5000), app)
    # http_server.serve_forever()