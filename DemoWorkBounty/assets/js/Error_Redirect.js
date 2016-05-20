
        $(document).ready(function () {
            var seconds = 10;
            var dvCountDown = document.getElementById("dvCountDown");
            var lblCount = document.getElementById("lblCount");
            dvCountDown.style.display = "block";
            lblCount.innerHTML = seconds;

            setInterval(function () {
                seconds--;
                lblCount.innerHTML = seconds;
               
                if (seconds == 0) {
                    dvCountDown.style.display = "none";
                    document.location.href = "/Home/Logout";
                }
            }, 1000);
        });
