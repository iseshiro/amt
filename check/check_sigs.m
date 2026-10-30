clear all;
global inv_leng sig_leng mic_ch sp_ch;
set_parameter
sig_sp = zeros(sig_leng + inv_leng - 1 , sp_ch);
Fs = 48000;
for mm = 1:mic_ch
    [sig_mic, Fs1] = audioread(sprintf('sig_mic_%d.wav', mm));
    for ss = 1:sp_ch
        %[inv, Fs2] = audioread(sprintf('inv_s%d_m%d.wav', ss, mm));
        fd = fopen(sprintf('inv_s%d_m%d.double', ss, mm),'rb');
        inv = fread( fd, 'double' );
        fclose(fd);
        xx = conv(sig_mic, inv);
        sig_sp(:, ss) = sig_sp(:, ss) + xx;
    end
end
dd_s = 0;
dd_n = 0;
for ss = 1:sp_ch
    [sig_sp1, Fs3] = audioread(sprintf('sig_sp_%d.wav', ss));
    dd = sig_sp1 - sig_sp(:, ss);
    dd_n = dd_n + sum(dd.^2);
    dd_s = dd_s + sum(sig_sp1.^2);
end
fprintf('Accuracy = %gdB\n', 10*log10(dd_s/dd_n));
